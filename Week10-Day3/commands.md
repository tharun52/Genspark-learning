## **1. Azure VM Setup**

Created a Linux Virtual Machine (Ubuntu 20.04 LTS)

---

## **2. SSH into the VM**

```bash
ssh keyname.pem azureuser@<vm-ip>
```

---

## **3. Install Docker on Azure VM**

```bash
sudo apt-get update
sudo apt-get install ca-certificates curl
sudo install -m 0755 -d /etc/apt/keyrings
sudo curl -fsSL https://download.docker.com/linux/ubuntu/gpg -o /etc/apt/keyrings/docker.asc
sudo chmod a+r /etc/apt/keyrings/docker.asc

echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.asc] https://download.docker.com/linux/ubuntu \
  $(. /etc/os-release && echo "${UBUNTU_CODENAME:-$VERSION_CODENAME}") stable" | \
  sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update
```

---

## ✅ **4. Create a .NET Web API Locally**

> Run these commands on your **local machine** (not inside the VM):

```bash
dotnet new webapi -o MyApiApp
cd MyApiApp
dotnet run
```
   
---

## ✅ **5. Create Dockerfile**

Create a `Dockerfile` inside `MyApiApp/`:

```dockerfile
FROM mcr.microsoft.com/dotnet/sdk:9.0@sha256:3fcf6f1e809c0553f9feb222369f58749af314af6f063f389cbd2f913b4ad556 AS build
WORKDIR /App

COPY . ./

RUN dotnet restore

RUN dotnet publish -o out

FROM mcr.microsoft.com/dotnet/aspnet:9.0@sha256:b4bea3a52a0a77317fa93c5bbdb076623f81e3e2f201078d89914da71318b5d8

WORKDIR /App

COPY --from=build /App/out .

EXPOSE 80
ENTRYPOINT ["dotnet", "MyApiApp.dll"]
```

---

## ✅ **6. Build and Push Docker Image**

```bash
docker build -t my-dotnet-api .
docker tag my-dotnet-api tharun52/my-dotnet-api:latest
docker login
docker push tharun52/my-dotnet-api:latest
```

---

## ✅ **7. Pull and Run the API on Azure VM**

```bash
sudo docker pull tharun52/my-dotnet-api:latest
sudo docker run -d -p 3000:8080 tharun52/my-dotnet-api:latest
sudo docker ps
```

---

## ✅ **8. Test the API**

Open your browser and go to:

```
http://<vm-ip>:3000/welcome
```
