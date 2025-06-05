<?php
require '../Models/creneaudata.php';

if ($_SERVER["REQUEST_METHOD"] == "GET" ) {
    if (isset($_GET['id'])) {
        $prestation_id = $_GET['id'];
    }
    // generate calendar

     $selected_date = isset($_GET['date']) ? $_GET['date'] : $calendrier[0]['date'];
        $timestamp = strtotime($selected_date);
        $week_day = date('N', $timestamp); // Lundi = 1
        $monday = strtotime("-" . ($week_day - 1) . " days", $timestamp); 
 for ($i = 0; $i < 7; $i++) {


        // Obtenir l'ID du jour pour la fonction
        $day_id_map = ['Monday'=>1, 'Tuesday'=>2, 'Wednesday'=>3, 'Thursday'=>4, 'Friday'=>5, 'Saturday'=>6, 'Sunday'=>7];
        $day_name = date('l', strtotime("+$i days", $monday));
        $day_id = $day_id_map[$day_name];

        $creneaux = GetCreneaux($prestation_id, $day_id);
        //generate the list of creneaux for the given prestation and day

    }
    $calendrier = generatecalendrier();

}
elseif ($_SERVER["REQUEST_METHOD"] == "POST" )
{
    $prestation_id = $_POST['prestation_id']?? 1;
    $selected_date = $_POST['selected_date'] ?? date('Y-m-d');
    $timestamp = strtotime($selected_date);
        $week_day = date('N', $timestamp); // Lundi = 1
     for ($i = 0; $i < 7; $i++) {


            // Obtenir l'ID du jour pour la fonction
            $day_id_map = ['Monday'=>1, 'Tuesday'=>2, 'Wednesday'=>3, 'Thursday'=>4, 'Friday'=>5, 'Saturday'=>6, 'Sunday'=>7];
            $day_name = date('l', strtotime("+$i days", $monday));
            $day_id = $day_id_map[$day_name];

            $creneaux = GetCreneaux($prestation_id, $day_id);
        }
    
}


    
    
require '../Views/creneau.php';