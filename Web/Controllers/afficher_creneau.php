<?php
// Simuler des données pour l'exemple
function generateCalendar($year, $month) {
    $calendar = [];
    $daysInMonth = cal_days_in_month(CAL_GREGORIAN, $month, $year);
    
    // Ajouter les jours du mois
    for ($day = 1; $day <= $daysInMonth; $day++) {
        $date = sprintf("%04d-%02d-%02d", $year, $month, $day);
        $calendar[] = [
            'date' => $date,
            'daynumber' => $day,
            'is_valid' => true
        ];
    }
    
    return $calendar;
}

function getCreneaux($date) {
    // Simuler des créneaux pour la démonstration
    $creneaux = [];
    $types = ['cabinet', 'visio'];
    
    // Générer des créneaux aléatoires
    for ($i = 8; $i < 18; $i++) { // De 8h à 18h
        if (rand(0, 3) > 0) { // 75% de chance d'avoir un créneau
            $type = $types[rand(0, 1)];
            $creneaux[] = [
                'time' => sprintf("%02d:%02d", $i, rand(0, 1)*30),
                'type' => $type,
                'cabinet' => $type === 'cabinet'
            ];
        }
    }
    
    return $creneaux;
}

// Traitement des paramètres
$cabinetChecked = isset($_GET['cabinet']) ? true : false;
$visioChecked = isset($_GET['visio']) ? true : false;

// Navigation par mois
$monthOffset = isset($_GET['month_offset']) ? (int)$_GET['month_offset'] : 0;
$currentDate = new DateTime();
$currentDate->modify("$monthOffset months");
$year = (int)$currentDate->format('Y');
$month = (int)$currentDate->format('n');
$monthName = $currentDate->format('F Y');

// Générer le calendrier
$calendar = generateCalendar($year, $month);

// Date sélectionnée
$selectedDate = $_GET['date'] ?? $calendar[0]['date'];
$creneaux = getCreneaux($selectedDate);

// Appliquer le filtrage
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
require '../Views/creneau.php';


