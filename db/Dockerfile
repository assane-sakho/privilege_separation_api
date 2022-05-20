FROM mysql:latest

# COPY ./queries/schema.sql /docker-entrypoint-initdb.d/0_schema.sql

COPY ./queries/schema.sql /docker-entrypoint-initdb.d/0_schema.sql
COPY ./queries/articles.sql /docker-entrypoint-initdb.d/1_articles.sql
COPY ./queries/regions.sql /docker-entrypoint-initdb.d/2_regions.sql
COPY ./queries/roles.sql /docker-entrypoint-initdb.d/3_roles.sql
COPY ./queries/role_region.sql /docker-entrypoint-initdb.d/4_role_region.sql
COPY ./queries/users.sql /docker-entrypoint-initdb.d/5_users.sql
COPY ./queries/role_user.sql /docker-entrypoint-initdb.d/6_role_user.sql
COPY ./queries/stores.sql /docker-entrypoint-initdb.d/7_stores.sql
COPY ./queries/article_store.sql /docker-entrypoint-initdb.d/8_article_store.sql
COPY ./queries/sales.sql /docker-entrypoint-initdb.d/9_sales.sql