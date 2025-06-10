<?php
require 'connexion.php';
function getcreneaux($prestation_id,$day_id){
    global $db;
    $query = $db->prepare('SELECT * FROM creneau WHERE prestation_id = :prestation_id AND day_id = :day_id order by starthour ASC');
    $query->bindParam(':prestation_id', $prestation_id);
    $query->bindParam(':day_id', $day_id);
    
    $query->execute();
    return $query->fetchAll();
    
}
function getcreneaubyid($creneau_id){
    global $db;
    $query = $db->prepare('SELECT * FROM creneau WHERE id = :creneau_id');
    $query->bindParam(':creneau_id', $creneau_id);
    
    $query->execute();
    return $query->fetch();
}
function getday_id($date){
     global $db;
    $query = $db->prepare('SELECT id FROM calendarday WHERE date = :date');
    $query->bindParam(':date', $date);
  
    $query->execute();
    return $query->fetch()['id'];
}

    function generatecalendar($currentyear, $currentmonth) {
    $calendar = array_fill(0, 42, null); // Grille vide (6x7)

    $firstDayOfMonth = date('N', strtotime("$currentyear-$currentmonth-01")); // 1 (Lundi) à 7 (Dimanche)
    $daysInMonth = cal_days_in_month(CAL_GREGORIAN, $currentmonth, $currentyear);

    for ($day = 1; $day <= $daysInMonth; $day++) {
        $date = sprintf('%04d-%02d-%02d', $currentyear, $currentmonth, $day);
        $dayIndex = $firstDayOfMonth - 1 + $day - 1; // Décalage pour bien positionner le premier jour

        $calendar[$dayIndex] = [
            'date' => $date,
            'day' => $dayIndex ,
            'day_of_week' => date('N', strtotime($date)),
            'is_valid' => true // Pour l’instant on suppose que tous les jours sont valides
        ];
    }

    return $calendar;
}


  