<?php
require '../Models/userdata.php';
require '../Models/creneaudata.php';
require '../vendor/autoload.php';
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;
if ($_SERVER['REQUEST_METHOD'] === 'GET') {
    if (isset($_GET['nom']) && isset($_GET['prenom']) && isset($_GET['email']) && isset($_GET['creneau_id'])) {
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
            $mail = new PHPMailer(true);
            $mail->isSMTP();
            $mail->Host = 'localhost';
            $mail->Port = 587;
            $mail->SMTPAuth = true;
            $mail->isMail();
            $mail->setFrom('zoglopiere20@gmail.com', 'Testeur');
            $mail->addAddress('peterzoglo@gmail.com');
            $mail->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
            $mail->Subject = 'Test local';
            $mail->Body = 'Ceci est un test sans SMTP.';

            if ($mail->send()) {
                echo "Mail envoyé  !";
            } else {
                echo "Erreur : " . $mail->ErrorInfo;
            }

        } catch (Exception $e) {
            echo "Erreur : {$mail->ErrorInfo}";
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