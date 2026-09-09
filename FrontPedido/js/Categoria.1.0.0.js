function ObtenerCategoria() {
  fetch("http://localhost:5194/api/Categoria")
    .then((respuesta) => respuesta.json())
    .then((data) => {
      console.log(data);
      mostrarCategoria(data);
    })
    .catch((error) => console.error(error));
}

function mostrarCategoria(data) {
  const tbody = document.getElementById("TablaCategoria");
  tbody.innerHTML = "";

  data.forEach((element) => {
    console.log("Elemento:", element);
    console.log("ID:", element.categoriaId);
    let tr = tbody.insertRow();
    tr.insertCell(0).innerHTML = element.nombres;

    // Botón eliminar
    let eliminar = document.createElement("button");
    eliminar.textContent = "Eliminar";

    eliminar.setAttribute(
      "onclick",
      `ValidacionEliminarCategoria(${element.categoriaId})`,
    );

    let tdEliminar = tr.insertCell(1);
    tdEliminar.appendChild(eliminar);

    // Botón editar
    let editar = document.createElement("button");

    editar.textContent = "Editar";
    editar.classList.add("btn", "btn-primary");

    editar.setAttribute(
      "onclick",
      `BuscarValoresCategoria(${element.categoriaId})`,
    );

    let tdEditar = tr.insertCell(2);
    tdEditar.appendChild(editar);
  });
}



function AgregarCategoria() {
    var nuevaCategoria = {
        nombres: document.getElementById("nombreCategoria").value
    };

    fetch("http://localhost:5194/api/Categoria", {
        method: "POST",
        headers: {
            Accept: "application/json",
            "Content-Type": "application/json"
        },
        body: JSON.stringify(nuevaCategoria)
    })
    .then((respuesta) => {
        if (!respuesta.ok) {
            throw new Error(`Error HTTP: ${respuesta.status}`);
        }

        return respuesta.text();
    })
    .then((data) => {
        console.log(data);

        document.getElementById("nombreCategoria").value = "";
        ObtenerCategoria();
    })
    .catch((error) => {
        console.error(error);
    });
}



function BuscarValoresCategoria(id) {
  fetch(`http://localhost:5194/api/Categoria/${id}`)
    .then((respuesta) => {
      if (!respuesta.ok) {
        throw new Error(`Error HTTP: ${respuesta.status}`);
      }
      return respuesta.json();
    })
    .then((data) => {
      console.log("Categoría:", data);

      document.getElementById("idEditar").value = data.categoriaId;
      document.getElementById("nombreEditar").value = data.nombres;

      let modal = new bootstrap.Modal(
        document.getElementById("editarCategoria"),
      );

      modal.show();
    })
    .catch((error) => {
      console.error("No se pudo acceder a la API:", error);
    });
}



function EditarCategoria() {
  let id = document.getElementById("idEditar").value;

  let editarCategoria = {
    categoriaId: document.getElementById("idEditar").value,
    nombres: document.getElementById("nombreEditar").value,
  };

  fetch(`http://localhost:5194/api/Categoria/${id}`, {
    method: "PUT",
    headers: {
      Accept: "application/json",
      "Content-Type": "application/json",
    },
    body: JSON.stringify(editarCategoria),
  })
    .then(() => {
      document.getElementById("idEditar").value = 0;
      document.getElementById("nombreEditar").value = "";
      let modal = bootstrap.Modal.getInstance(
        document.getElementById("editarCategoria"),
      );

      modal.hide();
      ObtenerCategoria();
    })
    .catch((error) => console.error("No se pudo editar la categoría.", error));
}



function ValidacionEliminarCategoria(id) {
  var siElimina = confirm("¿Esta seguro de eliminar esta Categoría?");
  if (siElimina == true) {
    EliminarCategoria(id);
  }
}
function EliminarCategoria(id) {
  fetch(`http://localhost:5194/api/Categoria/${id}`, {
    method: "DELETE",
  })
    .then(() => {
      ObtenerCategoria();
    })
    .catch((error) => console.error("No se pudo acceder a la api.", error));
}

ObtenerCategoria();