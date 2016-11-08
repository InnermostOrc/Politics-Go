<?php
if (!isset($_GET['id'])) {
	exit;
}
// Create connection
include_once('../ServerInformation/db.php');
$conn = new mysqli($DB_HOST, $DB_USER, $DB_PASS, $DB_NAME);
// Check connection
if ($conn->connect_error) {
    die("Connection failed: " . $conn->connect_error);
} 

$sql = "SELECT * FROM PollingStations WHERE ID='{$_GET['id']}'";
$result = $conn->query($sql);

if ($result->num_rows > 0) {
    // output data of each row
    $index = 0;
    while($row = $result->fetch_assoc()) {
        $polls[$index] = $row;
        $index++;
    }
} else {
    //echo "0 results";
}

$conn->close();
header('Content-Type: application/json');
echo json_encode($polls);
?>