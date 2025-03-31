using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;

namespace Crud.Controllers;

public class ContatosController : Controller
{
    private readonly ContatoDataAccess _dataAccess;

    public ContatosController(ContatoDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IActionResult Index()
    {
        try
        {
            var contatos = _dataAccess.ListarContatos();
            return View(contatos);
        }
        catch (Exception ex)
        {
            TempData["MensagenErro"] = "Ocorreu um erro na criação do Contato";
            return View();

        }


    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    public IActionResult Editar( int id)
    {
        var contato = _dataAccess.BuscarContatoPorId(id);
        return View(contato);
    }

    public IActionResult Detalhes(int id)
    {
        var contato = _dataAccess.BuscarContatoPorId(id);
        return View(contato);
    }

    public IActionResult Remover(int id)
    {
        var result = _dataAccess.Remover(id);
        if (result)
        {
            TempData["MensagemSucesso"] = "Contato removido com sucesso!";
        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um erro na remoção so Contato!";
        }
        return RedirectToAction("Index");
    }





    [HttpPost]
    public IActionResult Cadastrar(ContatoModel contato)
    {
        if (ModelState.IsValid)
        {
            var result = _dataAccess.Cadastrar(contato);
            
            if(result)
            {
                TempData["MensagemSucesso"] = "Contato criado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na criação do Contato!";
                return View(contato);
            }
        }
        else
        {
            return View(contato);
        }
    }

    [HttpPost]
    public IActionResult Editar(ContatoModel contato)
    {
        if (ModelState.IsValid)
        {
            var result = _dataAccess.Editar(contato);
            if (result)
            {
                TempData["MensagemSucesso"] = "Contato editado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na edição do Contato";
                return View(contato);
            }
        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um erro na edição so Contato";
            return View(contato);
        }
    }



}