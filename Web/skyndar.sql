drop database if exists skyndar;
create database if not exists skyndar;
use skyndar;

create table User (
    id int auto_increment primary key,
    username varchar(255) not null,
    password varchar(255) ,
    email varchar(255) not null,
    created_at timestamp default current_timestamp,
    IsAdmin boolean default false
);
create table day (
    id int auto_increment primary key,
    
    date date not null,
    number int not null,
    
);
create table Creneau(
    id int auto_increment primary key,
    day_id int not null,
    foreign key (day_id) references day(id),
    start_time time not null,
    end_time time not null,
    date date not null,
    cabinet boolean default true
   
);
create table Prestation(
    id int auto_increment primary key,
    titre varchar(255) not null,
    duree int not null,
    description text,
    tarif  int  not null

);
create table RendezVous(
    id int auto_increment primary key,
    user_id int not null,
    
    prestation_id int not null,
    created_at timestamp default current_timestamp,

   foreign key (prestation_id) references Prestation(id),
 
   foreign key (user_id) references User(id)
);
insert into User (username, password, email, IsAdmin) values
('Bertrand', 'admin', 'Bertrand@gmail.com', true);
