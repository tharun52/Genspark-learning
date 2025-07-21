import { Routes } from '@angular/router';
import { UploadVideoComponent } from './upload-video/upload-video';
import { HomeComponent } from './home/home';

export const routes: Routes = [
    { path: '', component: HomeComponent },
  { path: 'upload', component: UploadVideoComponent }
];
