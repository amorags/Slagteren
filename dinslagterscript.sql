create sequence customers_customer_id_seq
    as integer;

alter sequence customers_customer_id_seq owner to uxrlxmed;

create sequence shoppingcart_cart_id_seq
    as integer;

alter sequence shoppingcart_cart_id_seq owner to uxrlxmed;

create sequence passwords_password_id_seq;

alter sequence passwords_password_id_seq owner to uxrlxmed;

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
        constraint products_pk
            primary key,
    product_number     bigint       not null,
    product_name       varchar(150) not null,
    price_pr_kilo      numeric      not null,
    type_id            integer      not null
        constraint products_type_id_fk
            references ??? (),
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
        constraint customers_pk
            primary key,
    firstname varchar(50)                                                                   not null,
    lastname  varchar(50)                                                                   not null,
    email     varchar(100)                                                                  not null,
    address   varchar(100)                                                                  not null,
    zip       integer                                                                       not null,
    city      varchar(50)                                                                   not null,
    country   varchar(75)                                                                   not null,
    phone     integer                                                                       not null,
    "Role"    varchar(10) default 'Customer'::character varying                             not null
);

alter table users
    owner to uxrlxmed;

create table credit_card
(
    card_id        bigserial
        constraint credit_card_pk
            primary key,
    cardnumber     varchar(16)  not null,
    cardholdername varchar(120) not null,
    expirationdate date         not null,
    cvv            bigint       not null,
    customer_id    integer      not null
        constraint credit_card_customers_customer_id_fk
            references ??? ()
);

alter table credit_card
    owner to uxrlxmed;

create table shopping_cart
(
    cart_id     integer default nextval('dinslagter.shoppingcart_cart_id_seq'::regclass) not null
        constraint shoppingcart_pk
            primary key,
    customer_id bigint                                                                   not null
        constraint shoppingcart_customers_customer_id_fk
            references ??? (),
    created_at  date                                                                     not null,
    basket      varchar(255)
);

alter table shopping_cart
    owner to uxrlxmed;

drop table dinslagter.passwordhash;

create table dinslagter.passwordhash
(
    password_id   bigint PRIMARY KEY generated always as IDENTITY,
    password_hash varchar(350)                                                             not null,
    salt          varchar(250)                                                             not null,
    algorithm     varchar(50)                                                              not null,
    user_id       integer                                                                   not null
        constraint passwords_users_user_id_fk
            references dinslagter.users (user_id)
);

alter table passwordhash
    owner to uxrlxmed;

create table cart_items
(
    item_id    bigserial
        constraint cart_items_pk
            primary key,
    cart_id    bigint not null
        constraint cart__fk
            references ??? (),
    product_id bigint not null
        constraint product_id___fk
            references ??? (),
    quantity   bigint not null,
    price      bigint not null
);

alter table cart_items
    owner to uxrlxmed;


