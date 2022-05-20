# RBAC User Management

1. Clone the project:

```shell
  $ git clone https://github.com/assane-sakho/privilege_separation_api.git
```

2. Start `MySQL`, `PhpMyAdmin` via [docker-compose](https://docs.docker.com/compose/) in terminal:

```shell
  $ docker-compose up --build
```

3. Find the id of the launched app container
```shell
  $ docker ps
```

4. Connect to the app container
```shell
  $ docker exec -it --user root {id_of_the_app_container} /bin/bash
```

5. Start the service privilege-separation-api
```shell
  $ systemctl start privilege-separation-api
```

Url of phpmyadmin : http://localhost:8088/index.php?route=/database/structure&db=clothers
Url of api: http://127.0.0.1:8080/Sales