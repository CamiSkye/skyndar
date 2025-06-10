<!DOCTYPE html>
<html lang="fr">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Calendrier de rendez-vous</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css">
    <link rel="stylesheet" href="../Styles/creneau.css">
</head>
<body>
<header class="header">
    <div class="header-content">
        <img src="../Images/logo.png" alt="Logo de &kilibre" class="logo">
        <h2 class="responsable">Bertrand Mangin</h2>
        <h6 class="subtitle">Rendez-vous</h6>
    </div>
</header>
<section>
    <div class="calendar-container">
      
        <div class="navigation">
            <a href="?month=<?php echo $prevmonth; ?>&year=<?php echo $prevyear; ?>" class="prev-month"><i class="fas fa-chevron-left"></i></a>
            
            <?php echo date('F Y', strtotime(sprintf('%04d-%02d-01', $currentyear, $currentmonth))); ?>

            <a href="?month=<?php echo $nextmonth; ?>&year=<?php echo $nextyear; ?>" class="next-month"><i class="fas fa-chevron-right"></i></a>
        <table border ="1" cellpadding ="8" cellspacing ="0">
            <tr>
                <?php
               
                foreach ($daysOfWeek as $day) {
                    echo "<th>$day</th>";
                }
                ?>

            </tr>
            <tbody>
            <?php
                $count = 0;
                for ($i = 0; $i < 6; $i++) {
                    echo "<tr>";
                for ($j = 0; $j < 7; $j++) {
                    $cell = $calendar[$count] ?? null;
                    if ($cell && isset($cell['date'])) {
                             $date = $cell['date'];
                             $dayNumber = date('d', strtotime($date));

                        echo "<td><a href='?date=$date&month=$currentmonth&year=$currentyear'>  $dayNumber </a></td>";
                    } else {
                        echo "<td></td>"; // cellule vide
                    }
                        $count++;
                }   
                    echo "</tr>";
            }
            ?>
            </tbody>    
        </table>
       <?php

echo "<h2>Créneaux de la semaine du " . $lundi . "</h2>";
for ($i = 0; $i < 7; $i++) {
    $jour = date('Y-m-d', strtotime("$lundi +$i days"));
    $day_id = getday_id($jour);
    $creneauxDuJour = GetCreneaux($prestationId, $day_id);

    echo "<h3>" . date('d/m/Y', strtotime($jour)) . "</h3>";
    if ($creneauxDuJour) {
        echo "<ul>";
        foreach ($creneauxDuJour as $creneau) {
            echo "<a href='afficher_formulaire.php?creneau_id={$creneau['id']}'><li>{$creneau['starthour']} - {$creneau['endhour']}</li></a>";
}
        echo "</ul>";
    } else {
        echo "<p>Aucun créneau</p>";
    }
}
?>

</section>
</body>
</html>