# Docker

This will explain on how to use and install docker for your machine

## Install

### Windows
Download the exc form this link
> [Docker for windows](https://docs.docker.com/v17.12/docker-for-windows/install/#install-docker-for-windows-desktop-app)
### Mac
Download the mac app from this link.
> [Docker for mac](https://docs.docker.com/v17.12/docker-for-mac/install/)
### Linux
Install the following depency.
```sh
sudo apt-get install \
    apt-transport-https \
    ca-certificates \
    curl \
    gnupg-agent \
    software-properties-common
```

Add the docker repo 
```sh
sudo add-apt-repository \
   "deb [arch=amd64] https://download.docker.com/linux/ubuntu \
   $(lsb_release -cs) \
   stable"
```

Install the latetst version of docker
```sh
sudo apt-get install docker docker-cli docker.io
```

:::tip
#### Optional
If you want the docker service to start on startup you need to enable it after install.
```sh 
systemctl start docker
```
:::

## setup container

First up pull the container from my public repository.
``` sh
docker pull eddie013/allforone
```

## Troubleshooting

### Auth Error 
This error is because you have not yet log-in to docker.io 
``` sh
docker login docker.io
```

### Docker deamon
Cannot connect to the Docker daemon at unix:///var/run/docker.sock. Is the docker daemon running?
```sh
systemctl start docker
```