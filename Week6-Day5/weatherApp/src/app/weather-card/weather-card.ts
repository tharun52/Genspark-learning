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

description: string | null = null;
icon: string | null = null;
temp: number | null = null;
humidity: number | null = null;
windSpeed: number | null = null;
validData : boolean = false;
noData : boolean = true;
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
