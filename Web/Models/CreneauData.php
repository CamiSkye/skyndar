<?php
require 'connexion.php';
function GetCreneaux($prestation_id,$day_id){
    global $db;
    $query = $db->prepare('SELECT * FROM creneau WHERE prestation_id = :prestation_id AND day_id = :day_id order by starthour ASC');
    $query->bindParam(':prestation_id', $prestation_id);
    $query->bindParam(':day_id', $day_id);
    
    $query->execute();
    return $query->fetchAll();
    
}

  function generatecreneaux($id) {
        $jourcreneaux = [];
        $days = ['Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi', 'Dimanche'];
        for ($i = 0; $i < 7; $i++) {
            $jourcreneaux[] = [
                'day_id' => $i + 1,
                'day_name' => $days[$i],
                'creneaux' => GetCreneaux($id, $i + 1)
            ];
        }
        return $jourcreneaux ;
    }
    function generatecalendrier(){
        //
        $days=[];
        $current_month = date('m');
        $current_year = date('Y');
        $days_in_month = cal_days_in_month(CAL_GREGORIAN, $current_month, $current_year);
        $first_day_of_week = date('N', strtotime("$current_year-$current_month-01"));
        for ($day_num = 1; $day_num <= $days_in_month; $day_num++) {
            $current_date = sprintf('%04d-%02d-%02d', $current_year, $current_month, $day_num);
            $is_valid = false;
            
            $days[] = [
                    'daynumber' => $day_num,
                    'date' => $current_date,
                    'is_valid' => $is_valid
                ]; 
        }
        return $days;
    }