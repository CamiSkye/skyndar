<?php
require '../Models/userdata.php';
require '../Models/creneaudata.php';
require '../vendor/autoload.php';
use PHPMailer\PHPMailer\PHPMailer;
use PHPMailer\PHPMailer\Exception;
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


    
  
try {
    $mail = new PHPMailer(true);
    
    // Configuration SMTP (exemple pour Gmail)
    $mail->isSMTP();
    $mail->Host = 'smtp.gmail.com';
    $mail->SMTPAuth = true;
    $mail->Username = 'zoglopiere20@gmail.com'; // Votre adresse Gmail
    $mail->Password = 'Zog@2005@01.!?'; // Mot de passe ou "App Password"
    $mail->SMTPSecure = PHPMailer::ENCRYPTION_STARTTLS;
    $mail->Port = 587;

    // Destinataire et contenu
    $mail->setFrom('zoglopiere20@gmail.com', 'ZOGLO PIERRE');
    $mail->addAddress('peterzoglo@gmail.com');
    $mail->Subject = 'Test PHPMailer';
    $mail->Body = 'Ceci est un test.';

    $mail->send();
    echo 'E-mail envoyé avec succès !';
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