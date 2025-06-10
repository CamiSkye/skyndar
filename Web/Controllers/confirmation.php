<?php
require '../Models/userdata.php';
require '../Models/creneaudata.php';
if($_SERVER['REQUEST_METHOD']=== 'GET'){
    if(isset($_GET['nom']) && isset($_GET['prenom']) && isset($_GET['email'])  && isset($_GET['creneau_id'])) {
        $nom = htmlspecialchars($_GET['nom']);
        $prenom = htmlspecialchars($_GET['prenom']);
        $email = htmlspecialchars($_GET['email']);
       
        $creneau_id = (int)$_GET['creneau_id'];

        // recuperer l'id d'utilisateur
        $user_id = getuserid($nom, $email);
        echo "User ID: $user_id<br>";
        if ($user_id && $creneau_id) {
            // Insérer le rendez-vous
            insertrendezvous($user_id, $creneau_id);
        } else {
            echo "Erreur lors de la création de l'utilisateur.";
            exit;
        }
    } else {
        echo "Paramètres manquants.";
        exit;
    }

    $creneau = getcreneaubyid($creneau_id);
    if (!$creneau) {
        echo "Creneau non trouvé.";
        exit;
    }
} else {
    echo "Invalid request method.";
}
require '../Views/confirmation.php';