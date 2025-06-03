drop database if exists skyndar;
create database if not exists skyndar;
use skyndar;

create table user (
    id int auto_increment primary key,
    username varchar(255) not null,
    password varchar(255) ,
    email varchar(255) not null,
    created_at timestamp default current_timestamp,
    IsAdmin boolean default false
);
create table calendarday (
    id int auto_increment primary key,
    date date not null,
    daynumber int not null,
    isvalid  boolean default true
    
);

create table prestation(
    id int auto_increment primary key,
    titre varchar(255) not null,
    duree int not null,
    description text,
    tarif  int  not null

);
create table creneau(
    id int auto_increment primary key,
    day_id int not null,
    prestation_id int not null,
    starthour time not null,
    endhour time not null,
    cabinet boolean default true,
    foreign key (prestation_id) references prestation(id),
    foreign key (day_id) references calendarday(id)
   
);
create table rendezvous(
    id int auto_increment primary key,
    user_id int not null,
    
    prestation_id int not null,
    created_at timestamp default current_timestamp,

   foreign key (prestation_id) references prestation(id),
 
   foreign key (user_id) references user(id)
);
insert into user (username, password, email, IsAdmin) values
('Bertrand', 'admin', 'Bertrand@gmail.com', true);
