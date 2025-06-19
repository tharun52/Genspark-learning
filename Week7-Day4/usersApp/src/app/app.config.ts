import { ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';

import { routes } from './app.routes';
import { provideHttpClient } from '@angular/common/http';
import { UserService } from './services/UserService';
import { provideCharts, withDefaultRegisterables } from 'ng2-charts';
import { NgxEchartsModule } from 'ngx-echarts';

export const appConfig: ApplicationConfig = {
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient(),
    UserService, provideCharts(withDefaultRegisterables()),
    importProvidersFrom(
      NgxEchartsModule.forRoot({
        echarts: () => import('echarts')
      })
    )
  ]
};
