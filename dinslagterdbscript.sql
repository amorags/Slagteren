create table product_types
(
    type_id   serial
        primary key,
    type_name varchar(50) not null
);

alter table product_types
    owner to uxrlxmed;

create table products
(
    product_id         bigserial
        primary key,
    product_number     bigint       not null,
    product_name       varchar(150) not null,
    price_pr_kilo      numeric      not null,
    type_id            integer      not null
        constraint products_type_id_fk
            references product_types,
    country_of_birth   varchar(50)  not null,
    production_country varchar(50)  not null,
    description        varchar(255) not null,
    img_url            varchar(150) not null,
    min_exp_date       numeric      not null
);

alter table products
    owner to uxrlxmed;

create table users
(
    user_id   integer     default nextval('dinslagter.customers_customer_id_seq'::regclass) not null
        primary key,
    firstname varchar(50)                                                                   not null,
    lastname  varchar(50)                                                                   not null,
    email     varchar(100)                                                                  not null,
    address   varchar(100)                                                                  not null,
    zip       integer                                                                       not null,
    city      varchar(50)                                                                   not null,
    country   varchar(75)                                                                   not null,
    phone     integer                                                                       not null,
    role      varchar(10) default 'Customer'::character varying                             not null
);

alter table users
    owner to uxrlxmed;

create table shopping_cart
(
    cart_id     integer default nextval('dinslagter.shoppingcart_cart_id_seq'::regclass) not null
        primary key,
    customer_id bigint                                                                   not null
        constraint shoppingcart_customers_customer_id_fk
            references users,
    created_at  date                                                                     not null,
    basket      varchar(255)
);

alter table shopping_cart
    owner to uxrlxmed;

create table cart_items
(
    item_id    bigserial
        primary key,
    cart_id    bigint not null
        constraint cart_items_cart_fk
            references shopping_cart,
    product_id bigint not null
        constraint cart_items_product_fk
            references products,
    quantity   bigint not null,
    price      bigint not null
);

alter table cart_items
    owner to uxrlxmed;

create table passwordhash
(
    password_id   bigint generated always as identity
        primary key,
    password_hash varchar(350) not null,
    salt          varchar(250) not null,
    algorithm     varchar(50)  not null,
    user_id       integer      not null
        constraint passwords_users_user_id_fk
            references users
);

alter table passwordhash
    owner to uxrlxmed;


