<?php
require '../Models/creneaudata.php';
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    
   
    $prestationId = isset($_GET['prestation_id']) ? (int)$_GET['prestation_id'] : 1; 
    $cabinetChecked = isset($_GET['cabinet']) && $_GET['cabinet'] === 'on';
    $visioChecked = isset($_GET['visio']) && $_GET['visio'] === 'on';
 $currentmonth =isset($_GET['month']) ? (int)$_GET['month'] :date('m'); 
   
$currentyear = isset($_GET['year']) ? (int)$_GET['year'] : date ('Y'); 
$selectedDate = isset($_GET['date']) ? $_GET['date'] : date('Y-m-d');

$prevmonth = $currentmonth - 1;
$prevyear = $currentyear;
if ($prevmonth == 0) {
    $prevmonth = 12;
    $prevyear--;
}

$nextmonth = $currentmonth + 1;
$nextyear = $currentyear;
if ($nextmonth == 13) {
    $nextmonth = 1;
    $nextyear++;
}

 $daysOfWeek = ['Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi', 'Dimanche'];
$calendar = generatecalendar($currentyear, $currentmonth); 
$day_id = getday_id($selectedDate??date('Y-m-d'));
$creneaux = getcreneaux($prestationId, $day_id ); 

$dayOfWeek = date('N', strtotime($selectedDate)); 


$lundi = date('Y-m-d', strtotime($selectedDate . ' -' . ($dayOfWeek - 1) . ' days'));



$filteredCreneaux = [];
foreach ($creneaux as $c) {
    if ($cabinetChecked && !$visioChecked && $c['cabinet']) {
        $filteredCreneaux[] = $c;
    } elseif (!$cabinetChecked && $visioChecked && !$c['cabinet']) {
        $filteredCreneaux[] = $c;
    } elseif (($cabinetChecked && $visioChecked) || (!$cabinetChecked && !$visioChecked)) {
        $filteredCreneaux[] = $c;
    }
}
} 





require '../Views/creneau.php';


