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

        // Récupère le créneau pour l'affichage
        $creneau = getcreneaubyid($creneau_id);
        $date = $creneau['date'];
        $prestation = $creneau['titre'];
        if (!$creneau) {
            echo "Créneau non trouvé.";
            exit;
        }


        $heure_debut = $creneau['starthour'] ?? 'Heure inconnue';
        $heure_fin = $creneau['endhour'] ?? 'Heure inconnue';
        $lieu = "L'Embelie, 13 rue Pottier, Le Chesnay-Rocquencourt";

        try {
            $mailClient = new PHPMailer(true);
            $mailClient->CharSet = 'UTF-8';
            $mailClient->Encoding = 'base64';
            $mailClient->SMTPDebug = 0;
            $mailClient->isSMTP();
            $mailClient->Host = 'smtp.gmail.com';
            $mailClient->SMTPAuth = true;
            $mailClient->Username = 'zoglopiere20@gmail.com';
            $mailClient->Password = 'wqfn zcpx qiha cnyc';
            $mailClient->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
            $mailClient->Port = 587;


            $mailClient->setFrom('zoglopiere20@gmail.com', 'Skyndar');
            $mailClient->addAddress($email, "$prenom $nom");
            //Après le test mettre ($email, "$prenom $nom")
            $mailClient->addAddress('zoglopiere20@gmail.com', 'Skyndar'); // Pour envoyer une copie au prestataire
            $mailClient->isHTML(true);
            $mailClient->Subject = 'Confirmation rendez-vous';
            $mailClient->Body = "Bonjour,<br><br>"
                . "Merci pour votre réservation. Voici les détails de votre rendez-vous: <br>"
                . "<strong>Date :</strong> $date<br>"
                . "<strong>Heure :</strong> $heure_debut - $heure_fin <br>"
                . "<strong>Lieu :</strong> $lieu<br><br>"
                . "<strong>Prestation :</strong> $prestation<br>"
                . "Je vous remercie pour votre confiance. <br><br>"
                . "📩 Pour toute modification ou annulation, merci de me contacter par e-mail à : <a href='mailto:contact@e-ki-libre.net'>contact@e-ki-libre.net</a><br>"
                . "📄 Consultez la politique d’annulation/modification ici : <a href='https://e-ki-libre.net/tarifs/'>https://e-ki-libre.net/tarifs/</a><br>"

                . "À très bientôt, <br>"
                . "Bertrand MANGIN";
            $mailClient->send();
        } catch (Exception $e) {
            echo "Le mail n'a pas pu être envoyé. Mail Erreur: {$mailClient->ErrorInfo}";
        }



    } else {
        echo "Paramètres manquants.";
        exit;
    }
}

require '../Views/confirmation.php';