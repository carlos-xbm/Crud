using System.Diagnostics;
using Crud.Data;
using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace Crud.Controllers;

public class UsuariosController : Controller
{
    private readonly UsuarioDataAccess _dataAccess;

    public UsuariosController(UsuarioDataAccess dataAccess)
    {
        _dataAccess = dataAccess;
    }

    public IActionResult Index()
    {
        try
        {
            var usuarios = _dataAccess.ListarUsuarios();
            return View(usuarios);
        }
        catch (Exception ex)
        {
            TempData["MensagenErro"] = "Ocorreu um erro na cria��o do usu�rio";
            return View();

        }


    }

    public IActionResult Cadastrar()
    {
        return View();
    }

    public IActionResult Editar( int id)
    {
        var usuario = _dataAccess.BuscarUsuarioPorId(id);
        return View(usuario);
    }

    public IActionResult Detalhes(int id)
    {
        var usuario = _dataAccess.BuscarUsuarioPorId(id);
        return View(usuario);
    }

    public IActionResult Remover(int id)
    {
        var result = _dataAccess.Remover(id);
        if (result)
        {
            TempData["MensagemSucesso"] = "Usu�rio removido com sucesso!";
        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um erro na remo��o so usu�rio!";
        }
        return RedirectToAction("Index");
    }





    [HttpPost]
    public IActionResult Cadastrar(UsuarioModel usuario)
    {
        if (ModelState.IsValid)
        {
            var result = _dataAccess.Cadastrar(usuario);
            
            if(result)
            {
                TempData["MensagemSucesso"] = "Usu�rio criado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na cria��o do usu�rio!";
                return View(usuario);
            }
        }
        else
        {
            return View(usuario);
        }
    }

    [HttpPost]
    public IActionResult Editar(UsuarioModel usuario)
    {
        if (ModelState.IsValid)
        {
            var result = _dataAccess.Editar(usuario);
            if (result)
            {
                TempData["MensagemSucesso"] = "Usu�rio editado com sucesso!";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["MensagemErro"] = "Ocorreu um erro na edi��o do usu�rio";
                return View(usuario);
            }
        }
        else
        {
            TempData["MensagemErro"] = "Ocorreu um erro na edi��o so usu�rio";
            return View(usuario);
        }
    }



}