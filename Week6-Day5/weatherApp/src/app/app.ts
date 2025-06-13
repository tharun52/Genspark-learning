import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { WeatherDashboard } from "./weather-dashboard/weather-dashboard";

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, WeatherDashboard],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected title = 'weatherApp';
}
