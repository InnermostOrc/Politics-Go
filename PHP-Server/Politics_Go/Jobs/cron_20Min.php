<?php
function randUKLat() {
	$lat = rand(50, 58);
	if ($lat == 50) {
		$decLat = rand(415534, 999999);
	} else if ($lat == 58) {
		$decLat = rand(000000, 669078);
	} else {
		$decLat = rand(000000, 999999);
	}
	$latitude = (string)$lat . "." . (string)$decLat;
	return $latitude;
}
function randUKLong() {
	$longi = rand(-6, 2);
	if ($longi == 2) {
		$decLongi = rand(067389, 000000);
	} else if ($longi == -6) {
		$decLongi = rand(000000, 418211);
	} else {
		$decLongi = rand(000000, 999999);
	}
	$longitude = (string)$longi . "." . (string)$decLongi;
	return $longitude;
}
$creatureCount = 100;
$i = $creatureCount;
include_once('../ServerInformation/db.php');
$conn = new mysqli($servername, $username, $password, $dbname);
$sql = "SELECT * FROM Creatures";
$result = $conn->query($sql);
$idMin = 001;
$idMax = 001;
if ($result->num_rows > 0) {
	while($row = $result->fetch_assoc()) {
		$killTime = $row['Created'] + (20 * 60);
		if (time() >= $killTime) {
			$sqlDel = "DELETE FROM `Creatures` WHERE 1";
			if ($conn->query($sqlDel) === TRUE) {
			}
		}
	}
}
while ($i > 0) {
	$rarity = rand(0,5);
	if ($rarity <= 2) {
		$rarity = 0;
	} else if (($rarity >= 2) && ($rarity <= 4)) {
		$rarity = 1;
	} else if (($rarity >= 4) && ($rarity <= 5)) {
		$rarity = 2;
	}
	$DataID = (float)rand($idMin, $idMax);
	$ID = (string)$DataID;
	if (strlen($ID) == 1) {
		$ID = "00" . $ID;
	} else if (strlen($ID) == 2) {
		$ID = "0" . $ID;
	}
	$CreatureID = (string)$ID . "." . (string)$rarity;
	echo "ID = " . $ID . " Rarity = " . (string)$rarity . " All = " . (string)$CreatureID;
	$LAT = randUKLat();
	$LON = randUKLong();
	$CREATED = time();
	$sql = "INSERT INTO Creatures (`ID`, `Name`, `Lat`, `Longi`, `Created`)
	VALUES ('{$CreatureID}', 'Generic Creature', '{$LAT}', '{$LON}', '{$CREATED}')";
	if ($conn->query($sql) === TRUE) {
	} else {
	}
        $i--;
}
$conn->close();
?>