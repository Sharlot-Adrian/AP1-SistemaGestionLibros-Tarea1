using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests;

[TestClass]
public class PrestamosTests : PageTest
{
    private const string BaseUrl = "http://localhost:5235";

    [TestMethod]
    public async Task CrearPrestamo_Exitoso()
    {
        // 1. Navegar a la pantalla de crear préstamo
        await Page.GotoAsync($"{BaseUrl}/Prestamo/Create");

        // 2. Seleccionar el primer estudiante y libro disponibles en los InputSelect
        // Seleccionamos la opción por índice 1 (para evitar la opción "Elija una opción" que es índice 0)
        await Page.Locator("select.form-select").Nth(0).SelectOptionAsync(new[] { new SelectOptionValue { Index = 1 } });
        await Page.Locator("select.form-select").Nth(1).SelectOptionAsync(new[] { new SelectOptionValue { Index = 1 } });

        // 3. Hacer clic en el botón Guardar
        await Page.GetByRole(AriaRole.Button, new() { Name = "Guardar" }).ClickAsync();

        // 4. Verificar redirección al Index de préstamos
        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);
        await Expect(Page.GetByText("Registro de Prestamos")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task FiltrarPrestamos_PorEstudiante()
    {
        // 1. Ir al Index
        await Page.GotoAsync($"{BaseUrl}/Prestamo/Index");

        // 2. Seleccionar "Estudiante" en el filtro
        await Page.Locator("select.form-select").SelectOptionAsync(new[] { "Estudiante" });

        // 3. Escribir en el campo de búsqueda
        await Page.GetByPlaceholder("Buscar").FillAsync("Juan");

        // 4. Hacer clic en el botón de buscar
        await Page.Locator("button.bi-search").ClickAsync();

        // 5. Verificar que la tabla o el contador sigan visibles
        await Expect(Page.GetByText("Cantidad:")).ToBeVisibleAsync();
    }

    [TestMethod]
    public async Task RealizarDevolucion_Exitoso()
    {
        // 1. Ir al Index
        await Page.GotoAsync($"{BaseUrl}/Prestamo/Index");

        // 2. Hacer clic en el primer botón de devolución que aparezca en la QuickGrid
        var botonDevolucion = Page.Locator("a.bi-box-arrow-in-left").First;

        if (await botonDevolucion.IsVisibleAsync())
        {
            await botonDevolucion.ClickAsync();

            // 3. Confirmar la devolución haciendo clic en "Modificar"
            await Page.GetByRole(AriaRole.Button, new() { Name = "Modificar" }).ClickAsync();

            // 4. Validar redirección de vuelta al Index
            await Page.WaitForURLAsync($"{BaseUrl}/Prestamo/Index");
            await Expect(Page.GetByText("Registro de Prestamos")).ToBeVisibleAsync();
        }
    }

    [TestMethod]
    public async Task EliminarPrestamo_ConModal()
    {
        // 1. Ir al Index
        await Page.GotoAsync($"{BaseUrl}/Prestamo/Index");

        // 2. Hacer clic en el primer botón de edición (lápiz)
        var botonEditar = Page.Locator("a.bi-pencil-square").First;

        if (await botonEditar.IsVisibleAsync())
        {
            await botonEditar.ClickAsync();

            // 3. Hacer clic en el botón Eliminar para abrir el Modal Bootstrap
            await Page.GetByRole(AriaRole.Button, new() { Name = "Eliminar" }).ClickAsync();

            // 4. Confirmar la eliminación dentro del Modal
            await Page.GetByRole(AriaRole.Button, new() { Name = "Confirmar" }).ClickAsync();

            // 5. Validar que volvió a la tabla de Index
            await Page.WaitForURLAsync($"{BaseUrl}/Prestamo/Index");
            await Expect(Page.GetByText("Registro de Prestamos")).ToBeVisibleAsync();
        }
    }
}
