<?php
// Create connection
include_once('../ServerInformation/db.php');
$conn = new mysqli($DB_HOST, $DB_USER, $DB_PASS, $DB_NAME);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
}

$username = $_GET['usr'];
$password = $_GET['pas'];
//$username = "Rover656";
//$password = "rere1901";

$sql = "SELECT * FROM Users WHERE `username` LIKE '{$username}' AND `password` LIKE '{$password}' LIMIT 1";
$result = $conn->query($sql);
if ($result->num_rows > 0) {
	echo 1;
} else {
	echo 0;
}
?>