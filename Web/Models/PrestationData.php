<?php
require 'connexion.php';
function GetPrestations() {
    global $db;
    $query = $db->prepare('SELECT * FROM prestations  order by id ASC');
    
    $query->execute();
    return $query->fetchAll();
}