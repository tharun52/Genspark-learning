import { Component, OnInit } from '@angular/core';
import { VideoService } from '../Services/video.service';
import { TrainingVideo } from '../Models/TrainingVideo';
import { DatePipe } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-home',
  imports: [DatePipe, RouterLink],
  templateUrl: './home.html',
  styleUrl: './home.css'
})
export class HomeComponent implements OnInit {
  videos: TrainingVideo[] = [];
  selectedVideoUrl: string | null = null;

  loading: boolean = true;


  constructor(private videoService: VideoService) {}

  ngOnInit(): void {
    this.loading = true;
    this.videoService.getVideos().subscribe(data => {
      this.videos = data;
      this.loading = false;
    }, error => {
      this.loading = false;
      console.error('Error loading videos', error);
    });
  }

  openModal(url: string) {
    this.selectedVideoUrl = url;
  }

  closeModal() {
    this.selectedVideoUrl = null;
  }
  deleteVideo(videoId: string) {
    if (confirm('Are you sure you want to delete this video?')) {
      this.videoService.deleteVideo(videoId).subscribe(() => {
        this.videos = this.videos.filter(v => v.id !== videoId);
      });
    }
  }
}