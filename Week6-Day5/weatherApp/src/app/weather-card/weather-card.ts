import { JsonPipe } from '@angular/common';
import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-weather-card',
  imports: [JsonPipe],
  templateUrl: './weather-card.html',
  styleUrl: './weather-card.css'
})
export class WeatherCard {
  @Input() weather: any;

description: string = '';
icon: string = '';
temp: number = 0;
humidity: number = 0;
windSpeed: number = 0;

ngOnChanges() {
  if (this.weather) {
    this.description = this.weather.weather?.[0]?.description ?? '';
    this.icon = this.weather.weather?.[0]?.icon ?? '';
    this.temp = this.weather.main?.temp ?? 0;
    this.humidity = this.weather.main?.humidity ?? 0;
    this.windSpeed = this.weather.wind?.speed ?? 0;
  }
}

}
