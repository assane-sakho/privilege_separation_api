  CREATE TABLE `articles`  (
    `id` int auto_increment,
    `name` varchar(255) NULL,
    `description` TEXT NULL,
    `price` decimal(10, 2) NULL,
    PRIMARY KEY (`id`)
  );

  CREATE TABLE `regions`  (
    `id` int auto_increment,
    `name` varchar(255) NULL,
    PRIMARY KEY (`id`)
  );

  CREATE TABLE `roles`  (
    `id` int auto_increment,
    `libelle` varchar(255) NULL,
    PRIMARY KEY (`id`)
  );

  CREATE TABLE `role_region`  (
    `role_id` int NOT NULL,
    `region_id` int NOT NULL,
    PRIMARY KEY (`role_id`, `region_id`)
  );

  CREATE TABLE `role_user`  (
    `role_id` int NOT NULL,
    `user_id` int NOT NULL,
    PRIMARY KEY (`role_id`, `user_id`)
  );

  CREATE TABLE `sales`  (
    `id` int auto_increment,
    `date` datetime NULL,
    `price` decimal(10, 2) NULL,
    `amount` int NULL,
    `article_store_id` int NULL,
    `client_reference` varchar(255),
    PRIMARY KEY (`id`)
  );

  CREATE TABLE `stores`  (
    `id` int auto_increment,
    `name` varchar(255) NULL,
    `address` varchar(255) NULL,
    `region_id` int NULL,
    PRIMARY KEY (`id`)
  );

  CREATE TABLE `article_store`  (
    `id` int auto_increment,
    `article_id` int NULL,
    `store_id` int NULL,
    `stock` varchar(255) NULL,
    PRIMARY KEY (`id`)
  );

  CREATE TABLE `users`  (
    `id` int auto_increment,
    `first_name` varchar(255) NULL,
    `last_name` varchar(255) NULL,
    `token` varchar(255) NULL,
    `job` varchar(255) NULL,
    PRIMARY KEY (`id`)
  );

  ALTER TABLE `article_store` ADD CONSTRAINT `fk_article_store_store_1` FOREIGN KEY (`store_id`) REFERENCES `stores` (`id`);
  ALTER TABLE `article_store` ADD CONSTRAINT `fk_article_store_article` FOREIGN KEY (`article_id`) REFERENCES `articles` (`id`);
  ALTER TABLE `stores` ADD CONSTRAINT `fk_store_region_1` FOREIGN KEY (`region_id`) REFERENCES `regions` (`id`);
  ALTER TABLE `role_region` ADD CONSTRAINT `fk_role_role_region_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`);
  ALTER TABLE `role_region` ADD CONSTRAINT `fk_region_role_region_1` FOREIGN KEY (`region_id`) REFERENCES `regions` (`id`);
  ALTER TABLE `role_user` ADD CONSTRAINT `fk_role_role_user_1` FOREIGN KEY (`role_id`) REFERENCES `roles` (`id`);
  ALTER TABLE `role_user` ADD CONSTRAINT `fk_user_role_user_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`);
  ALTER TABLE `sales` ADD CONSTRAINT `fk_sale_article_store_1` FOREIGN KEY (`article_store_id`) REFERENCES `article_store` (`id`);