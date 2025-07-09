<?php
require '../Models/userdata.php';
require '../Models/creneaudata.php';
require '../vendor/autoload.php';

use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;



if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (isset($_GET['nom'], $_GET['prenom'], $_GET['email'], $_GET['creneau_id'])) {
        $nom = htmlspecialchars($_GET['nom']);
        $prenom = htmlspecialchars($_GET['prenom']);
        $email = htmlspecialchars($_GET['email']);
        $creneau_id = (int) $_GET['creneau_id'];

        $user_id = getuserid($nom, $email);

        if ($user_id && $creneau_id) {
            insertrendezvous($user_id, $creneau_id);
        } else {
            echo "Erreur lors de la création de l'utilisateur.";
            exit;
        }

        // Récupère le créneau pour l'affichage
        $creneau = getcreneaubyid($creneau_id);
        if (!$creneau) {
            echo "Créneau non trouvé.";
            exit;
        }


        $heure_debut = $creneau['starthour'] ?? 'Heure inconnue';
        $heure_fin = $creneau['endhour'] ?? 'Heure inconnue';
        $lieu = "L'Embelie, 13 rue Pottier, Le Chesnay-Rocquencourt";

        try {
            $mailPro = new PHPMailer(true);
            $mailPro->isSMTP();
            $mailPro->Host = 'smtp.gmail.com';
            $mailPro->SMTPAuth = true;
            $mailPro->Username = 'zoglopiere20@gmail.com';
            $mailPro->Password = 'wqfn zcpx qiha cnyc';
            $mailPro->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
            $mailPro->Port = 587;

            $mailPro->setFrom('zoglopiere20@gmail.com', 'Skyndar');
            $mailPro->addAddress($email, $nom);
            $mailPro->addAddress('zoglopiere20@gmail.com', 'Skyndar');

            $mailPro->isHTML(true);
            $mailPro->Subject = 'Nouveau rendez-vous réservé';
            $mailPro->Body = "Un nouveau RDV vient d’être réservé !<br><br>"
                . "<strong>Client :</strong> $prenom $nom<br>"
                . "<strong>Email :</strong> $email<br>"
                . "<strong>Heure :</strong> $heure_debut - $heure_fin <br>"
                . "<strong>Lieu :</strong> $lieu<br><br>"
                . "Merci de bien vouloir préparer ce créneau.";

            $mailPro->send();

        } catch (Exception $e) {
            echo "Le mail n'a pas pu être envoyé. Erreur : {$mailPro->ErrorInfo}";
        }

    } else {
        echo "Paramètres manquants.";
        exit;
    }
}

require '../Views/confirmation.php';