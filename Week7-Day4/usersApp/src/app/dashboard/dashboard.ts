import { Component } from '@angular/core';
import { UserModel } from '../models/UserModel';
import { FilterModel } from '../models/FilterModel';
import { UserService } from '../services/UserService';
import { FormsModule } from '@angular/forms';
import { NgxEchartsDirective, NgxEchartsModule } from 'ngx-echarts';


@Component({
  selector: 'app-dashboard',
  imports: [FormsModule, NgxEchartsDirective],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {
 users: UserModel[] = [];
  filter: FilterModel = new FilterModel();

  genderChartOptions: any;
  roleChartOptions: any;
  ageChartOptions: any;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userService.users$.subscribe(users => {
      this.users = users;
      this.updateCharts();
    });
    this.userService.loadUsers();
  }

  updateCharts(): void {
    const filtered = this.applyFilters(this.users);

    const male = filtered.filter(u => u.gender === 'male').length;
    const female = filtered.filter(u => u.gender === 'female').length;

    this.genderChartOptions = {
      title: { text: 'Gender Distribution', left: 'center' },
      tooltip: { trigger: 'item' },
      legend: { bottom: 0 },
      series: [
        {
          type: 'pie',
          radius: '60%',
          data: [
            { value: male, name: 'Male' },
            { value: female, name: 'Female' }
          ]
        }
      ]
    };

    const roles = ['admin', 'moderator', 'user'];
    const roleCounts = roles.map(role => ({
      value: filtered.filter(u => u.role === role).length,
      name: role.charAt(0).toUpperCase() + role.slice(1)
    }));

    this.roleChartOptions = {
      title: { text: 'Role Distribution', left: 'center' },
      tooltip: { trigger: 'item' },
      legend: { bottom: 0 },
      series: [
        {
          type: 'pie',
          radius: ['40%', '70%'],
          data: roleCounts
        }
      ]
    };

    const ageGroups = [0, 0, 0, 0, 0];
    filtered.forEach(u => {
      if (u.age <= 18) ageGroups[0]++;
      else if (u.age <= 30) ageGroups[1]++;
      else if (u.age <= 45) ageGroups[2]++;
      else if (u.age <= 60) ageGroups[3]++;
      else ageGroups[4]++;
    });

    this.ageChartOptions = {
      title: { text: 'Age Group Distribution', left: 'center' },
      xAxis: {
        type: 'category',
        data: ['0-18', '19-30', '31-45', '46-60', '61+']
      },
      yAxis: { type: 'value' },
      series: [
        {
          data: ageGroups,
          type: 'bar',
          barWidth: '50%',
          itemStyle: {
            color: '#4285F4'
          }
        }
      ]
    };
  }

  applyFilters(users: UserModel[]): UserModel[] {
    let filtered = [...users];

    if (this.filter.gender)
      filtered = filtered.filter(u => u.gender === this.filter.gender);

    if (this.filter.role)
      filtered = filtered.filter(u => u.role === this.filter.role);

    if (this.filter.ageRange) {
      const [min, max] = this.filter.ageRange.includes('+')
        ? [parseInt(this.filter.ageRange), Infinity]
        : this.filter.ageRange.split('-').map(Number);
      filtered = filtered.filter(u => u.age >= min && u.age <= max);
    }

    return filtered;
  }

  onFilterChange(): void {
    this.updateCharts();
  }
}
