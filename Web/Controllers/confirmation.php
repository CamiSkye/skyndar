<?php
require '../Models/userdata.php';
require '../Models/creneaudata.php';
require '../vendor/autoload.php';

use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;



$prestataireEmail = 'testcodelily@gmail.com'; //Test pour voir si le prestataire reçoit le mail
$prestataireNom = 'Lily';

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

        try {
            $mailPro = new PHPMailer(true);
            $mailPro->isMail();
            $mailPro->setFrom('zoglopiere20@gmail.com', 'Skyndar');
            $mailPro->addAddress($prestataireEmail, $prestataireNom);
            $mailPro->isHTML(true);
            $mailPro->Subject = 'Nouveau rendez-vous réservé';
            $mailPro->Body = "Un nouveau RDV vient d’être réservé !\n\n"
                . "Client : $prenom $nom\n"
                . "Email : $email\n"
                . "Heure : $heure\n"
                . "Lieu : $lieu\n\n"
                . "Merci de bien vouloir préparer ce créneau";

            $mailPro->send();
            echo 'Mail bien envoyé !';
        } catch (Exception $e) {
            echo "Le mail n'a pas pu être envoyé. Mail Erreur: {$mailPro->ErrorInfo}";
        }

    } else {
        echo "Paramètres manquants.";
        exit;
    }

    $creneau = getcreneaubyid($creneau_id);
    if (!$creneau) {
        echo "Creneau non trouvé.";
        exit;
    } else {
        echo "Invalid request method.";
    }
}
require '../Views/confirmation.php';