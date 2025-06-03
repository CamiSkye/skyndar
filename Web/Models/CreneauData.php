<?php
require 'connexion.php';
function GetCreneaux() {
    global $db;
    $query = $db->prepare('SELECT * FROM creneau ORDER BY id ASC');
    
    $query->execute();
    return $query->fetchAll();
}