import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { WeatherService } from '../services/weather.service';
import { CitySearch } from "../city-search/city-search";
import { WeatherCard } from "../weather-card/weather-card";
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-weather-dashboard',
  imports: [CitySearch, WeatherCard, CommonModule],
  templateUrl: './weather-dashboard.html',
  styleUrl: './weather-dashboard.css'
})
export class WeatherDashboard {
  weatherData$: Observable<any>;

  constructor(private weatherService: WeatherService) {
    this.weatherData$ = this.weatherService.weather$;
  }
}
