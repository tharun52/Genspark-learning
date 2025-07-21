import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { TrainingVideo } from '../Models/TrainingVideo';


@Injectable()
export class VideoService {
  private apiUrl = 'http://localhost:5095/TrainingVideos'; 

  constructor(private http: HttpClient) {}

  getVideos(): Observable<TrainingVideo[]> {
    return this.http.get<TrainingVideo[]>(this.apiUrl);
  }
  uploadVideo(formData: FormData): Observable<any> {
    return this.http.post(`${this.apiUrl}/upload`, formData);
  }
  deleteVideo(videoId: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${videoId}`);
  }
}
