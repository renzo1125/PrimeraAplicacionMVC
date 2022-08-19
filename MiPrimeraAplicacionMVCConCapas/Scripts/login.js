

function Login() {
	var nombreusuario = getN("nombreusuario")
	var contra = getN("contra")
	fetchGet("Login/uspLogin/?usuario=" + nombreusuario + "&contra=" + contra, function (data) {

		if (data.iidusuario == 0) Error("Usuario o contra incorrecto")
		else {
			Correcto("Bienvenido");
			//http://localhost:50383/Cama/Index
			document.location.href = setUrl("Cama/Index");
		}

			
	})
}