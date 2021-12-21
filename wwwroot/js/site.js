let position;

window.onload = () => {
  document.querySelector("#btnRastrear").addEventListener("click", track);
};

async function track() {
  try {
    const position = await getLocation();
    const res = await fetch("Home/TrackMe", {
      body: JSON.stringify(position),
      method: "POST",
      headers: {
        "content-type": "application/json",
      },
    });
    var resultado = await res.json();
    document.querySelector("#resultado").innerHTML = `
        <p><b>IP:</b> ${resultado.ip}</p>
        <p><b>User Agent:</b> ${resultado.userAgent}</p>
        <p><b>Localização:</b> ${JSON.stringify(resultado.location)}</p>
    `;
  } catch (e) {
    console.error(e);
  }
}

async function getLocation() {
  return new Promise((resolve, reject) => {
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition((position) => {
        resolve({
          accuracy: position.coords.accuracy,
          altitude: position.coords.altitude,
          altitudeAccuracy: position.coords.altitudeAccuracy,
          heading: position.coords.heading,
          latitude: position.coords.latitude,
          longitude: position.coords.longitude,
          speed: position.coords.speed,
        });
      });
    } else {
      reject(new Error("Navegador não suporta API de Geolocalização."));
    }
  });
}
