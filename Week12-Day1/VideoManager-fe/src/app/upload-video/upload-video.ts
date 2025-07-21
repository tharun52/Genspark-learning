import { Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { VideoService } from '../Services/video.service';
import { Router } from '@angular/router';


@Component({
  selector: 'app-upload-video',
  imports: [FormsModule, ReactiveFormsModule],
  templateUrl: './upload-video.html',
  styleUrl: './upload-video.css'
})
export class UploadVideoComponent {
  uploadForm: FormGroup;
  loading = false;
  uploadSuccess = false;
  uploadError = '';

  constructor(private fb: FormBuilder, private videoService: VideoService, private router:Router) {
    this.uploadForm = this.fb.group({
      title: ['', Validators.required],
      description: ['', Validators.required],
      file: [null, Validators.required],
    });
  }

  onFileChange(event: any) {
    const file = event.target.files[0];
    if (file && file.type === 'video/mp4') {
      this.uploadForm.patchValue({ file });
    } else {
      this.uploadForm.patchValue({ file: null });
      alert('Only MP4 files are allowed.');
    }
  }

  onSubmit() {
    if (this.uploadForm.invalid) return;

    const formData = new FormData();
    formData.append('title', this.uploadForm.get('title')?.value);
    formData.append('description', this.uploadForm.get('description')?.value);
    formData.append('file', this.uploadForm.get('file')?.value);

    this.loading = true;
    this.uploadSuccess = false;
    this.uploadError = '';

    this.videoService.uploadVideo(formData).subscribe({
      next: () => {
        this.loading = false;
        this.uploadSuccess = true;
        this.uploadForm.reset();
        this.router.navigateByUrl('');
      },
      error: (err) => {
        this.loading = false;
        this.uploadError = err.error?.message || 'Upload failed.';
      },
    });
  }
}