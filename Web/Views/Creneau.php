
<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <meta http-equiv="X-UA-Compatible" content="ie=edge">
   
    <link rel="stylesheet" href="../Styles/creneau.css">
    <title>Document</title>
</head>
<body>
<header class="header">
    <div class="header-content">
        <img src="../Images/logo.png" alt="Logo" class="logo">
        <div>
            <h2 class="responsable">Bertrand Mangin</h2>
            <h6 class="subtitle">Rendez-vous</h6>
        </div>
    </div>
</header>

<section class="main-container">
    <div class="dashboard">
        <!-- Carte Calendrier -->
        <div class="calendar-card">
            <h2 class="card-title">Calendrier</h2>
            <div class="week-grid">
                <?php 
                $jours_semaine = ['Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi', 'Dimanche'];
                foreach ($jours_semaine as $jour) {
                    echo '<div class="day-header">'.substr($jour, 0, 3).'</div>';
                }
                
                foreach ($calendrier as $jour) {
                    $class = $jour["is_valid"] ? "valid" : "invalid";
                    echo '<a href="../Controllers/afficher_creneau.php?date='.$jour["date"].'" class="day-card '.$class.'">';
                    echo '<div class="date">'.$jour["daynumber"].'</div>';
                    echo '</a>';
                }
                ?>
            </div>
        </div>
        
        <!-- Carte Créneaux -->
        <div class="slots-card">
            <h2 class="card-title">Créneaux disponibles</h2>
            <?php
            for ($i = 0; $i < 7; $i++) {
                $date = date('Y-m-d', strtotime("+$i days", $monday));
                $day_name = date('l', strtotime($date));
                $day_id = $day_id_map[$day_name];
                $creneaux = GetCreneaux($prestation_id, $day_id);
                
                echo '<div class="day-slots">';
                echo '<h3 class="day-title">';
                echo '<span>'.ucfirst(strftime('%A %d/%m', strtotime($date))).'</span>';
                echo '</h3>';
                
                if (empty($creneaux)) {
                    echo '<p class="no-slots">Aucun créneau disponible</p>';
                } else {
                    echo '<ul class="slot-list">';
                    foreach ($creneaux as $c) {
                        echo '<li class="slot-item">';
                        echo substr($c['starthour'], 0, 5);
                        echo '</li>';
                    }
                    echo '</ul>';
                }
                echo '</div>';
            }
            ?>
        </div>
    </div>
</section>
</body>
</html>


