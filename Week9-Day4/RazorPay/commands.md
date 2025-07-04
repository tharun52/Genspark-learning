## Dockerizing Razor Pay Angular App

### Build Angular App

```bash
ng build --configuration production
```

---

### Build Docker Image

```bash
docker build -t razor-pay-app .
```

---

### Run Docker Container

```bash
docker run -d -p 4201:80 razor-pay-app
```

---
