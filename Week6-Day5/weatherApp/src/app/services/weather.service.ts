import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable()
export class WeatherService {
    private apikey = 'b5c0e4a9caf16e0e488ab0dc0e47f001';
    private weatherSubject = new BehaviorSubject<any>(null);
    weather$: Observable<any> = this.weatherSubject.asObservable();

    constructor(private http: HttpClient) { }

    fetchWeather(city: string) {
        if (!city || city.trim() === '') {
            this.weatherSubject.next(null);
            return;
        }

        const url = `https://api.openweathermap.org/data/2.5/weather?q=${city}&appid=${this.apikey}&units=metric`;
        this.http.get(url).subscribe({
            next: (data) => this.weatherSubject.next({ data }),
            error: () => this.weatherSubject.next({ error: true }),
        });
    }
}