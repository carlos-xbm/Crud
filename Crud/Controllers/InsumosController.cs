using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers;

public class InsumosController : Controller
{
    private readonly InsumoDataAccess _dataAccess;
    public InsumosController(InsumoDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }


    public IActionResult Index()
    {
        try
        {
            var insumos = _dataAccess.ListarInsumos();
            return View(insumos);
        }
        catch (Exception )
        {
            TempData["MensagemErro"] = "Ocorreu um erro na criação do insumo";
            return View();
        }
    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    public IActionResult Editar(int id)
    {
        var insumo = _dataAccess.BuscarInsumoPorId(id);
        return View(insumo);
    }

    public IActionResult Detalhes(int id)
    {
        var insumo = _dataAccess.BuscarInsumoPorId(id);
        return View(insumo);
    }

    public IActionResult Remover(int id)
    {
        var result = _dataAccess.Remover(id);
        if (result)
        {
            TempData["MensagemSucesso"] = "Imsumo removido com sucesso!";

        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um erro na remoção de insumos!";

        }
        return RedirectToAction("Index");
    }


    [HttpPost]
    public IActionResult Cadastrar(InsumoModel insumo)
    {
        if (ModelState.IsValid)
        {
            var result = _dataAccess.Cadastrar(insumo);

            if (result)
            {
                TempData["MensagemSucesso"] = "Insumo criado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na criação do Insumo!";
                return View(insumo);
            }
        }
        else
        {
            return View(insumo);
        }


    }

    [HttpPost]
    public IActionResult Editar(InsumoModel insumo)
    {
        if (ModelState.IsValid)
        {
            var result = _dataAccess.Editar(insumo);
            if (result)
            {
                TempData["MensagemSucesso"] = "Insumo Editado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na edição do insumo!";
                return View(insumo);
            }
        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um erro na edição do insumo!";
            return View(insumo);
        }
    }


}
