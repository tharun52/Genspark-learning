import { Component } from '@angular/core';
import { WeatherService } from '../services/weather.service';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-city-search',
  imports: [FormsModule],
  templateUrl: './city-search.html',
  styleUrl: './city-search.css'
})
export class CitySearch {
  city:string = '';
  constructor(private weatherService:WeatherService)
  {}
  search(){
    if(this.city.trim())
      this.weatherService.fetchWeather(this.city);
  }
}
