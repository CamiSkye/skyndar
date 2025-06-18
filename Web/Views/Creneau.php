<!DOCTYPE html>
<html lang="fr">

<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Calendrier de rendez-vous</title>

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
                <a href="?month=<?php echo $prevmonth; ?>&year=<?php echo $prevyear; ?>" class="prev-month"><i
                        class="fas fa-chevron-left">&lt</i></a>

                <?php echo date('F Y', strtotime(sprintf('%04d-%02d-01', $currentyear, $currentmonth))); ?>

                <a href="?month=<?php echo $nextmonth; ?>&year=<?php echo $nextyear; ?>" class="next-month"><i
                        class="fas fa-chevron-right">&gt</i></a>
                <table border="1" cellpadding="8" cellspacing="0">
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
                                    echo "<td></td>";
                                }
                                $count++;
                            }
                            echo "</tr>";
                        }
                        ?>
                    </tbody>
                </table>
            </div>
            <div class="filter-options">
                <form method="get" action="../Controllers/afficher_creneau.php">
                    <input type="hidden" name="prestation_id" value="<?php echo htmlspecialchars($prestationId); ?>">
                    <input type=hidden name=date value="<?php echo htmlspecialchars($selectedDate); ?>">
                    <label>
                        <input type="checkbox" name="cabinet" value="cabinet">
                        Cabinet
                    </label>
                    <label>
                        <input type="checkbox" name="visio" value="visio">
                        Visio
                    </label>
                    <button type="submit">Filtrer</button>
                </form>
            </div>
        </div>
        <?php

        echo "<h2>Créneaux de la semaine du " . $lundi . "</h2>";
        for ($i = 0; $i < 7; $i++) {
            $jour = date('Y-m-d', strtotime("$lundi +$i days"));

            $day_id = getday_id($jour);

            $cabinetChecked = isset($_GET['cabinet']) && $_GET['cabinet'];
            $creneauxDuJour = GetCreneaux(prestation_id: $prestationId, day_id: $day_id);
            $filteredCreneaux = [];
            foreach ($creneauxDuJour as $c) {
                if ($cabinetChecked && !$visioChecked && $c['cabinet']) {
                    $filteredCreneaux[] = $c;
                } elseif (!$cabinetChecked && $visioChecked && !$c['cabinet']) {
                    $filteredCreneaux[] = $c;
                } elseif (($cabinetChecked && $visioChecked) || (!$cabinetChecked && !$visioChecked)) {
                    $filteredCreneaux[] = $c;
                }
            }
            echo "<h3>" . date('d/m/Y', strtotime($jour)) . "</h3>";
            if ($filteredCreneaux) {
                echo "<ul>";
                foreach ($filteredCreneaux as $creneau) {
                    $reservedCreneaux = getreservedcreneaux($creneau['id']);
                    foreach ($reservedCreneaux as $rc) {
                        if ($rc['starthour'] == $creneau['starthour']) {
                            echo "<strike><li class='reserved'>{$creneau['starthour']} - {$creneau['endhour']} </li></strike>(Réservé)";
                            continue 2; // me permet de ne pas prendre en compte les créneaux déjà réservés, (saute les deux boucles)
                        }
                    }
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