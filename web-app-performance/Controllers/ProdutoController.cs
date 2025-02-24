﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Newtonsoft.Json;
using StackExchange.Redis;
using web_app_domain;
using web_app_repository;

namespace web_app_performance.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private static ConnectionMultiplexer redis;
        private readonly IProdutoRepository _repository;

        public ProdutoController(IProdutoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduto()
        {
            // string key = "getProduto";
            // redis = ConnectionMultiplexer.Connect("localhost:6379");
            // IDatabase db = redis.GetDatabase();
            // await db.KeyExpireAsync(key, TimeSpan.FromMinutes(10));
            // string user = await db.StringGetAsync(key);

            // if (!string.IsNullOrEmpty(user))
            // {
            //     return Ok(user);
            // }

            var produtos = await _repository.ListarProdutos();
            if(produtos == null) 
            {
                    return NotFound();
            }

            string produtosJson = JsonConvert.SerializeObject(produtos);
            //await db.StringSetAsync(key, produtosJson);

            return Ok(produtos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Produto produto)
        {
            await _repository.SalvarProduto(produto);

            // string key = "getProdutos";
            // redis = ConnectionMultiplexer.Connect("localhost:6379");
            // IDatabase db = redis.GetDatabase();
            // await db.KeyDeleteAsync(key);

            return Ok(new {mensagem = "Criado com sucesso"});
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Produto produto)
        {
            await _repository.AtualizarProduto(produto);

            string key = "getProdutos";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.RemoverProduto(id);

            string key = "getUsuarios";
            redis = ConnectionMultiplexer.Connect("localhost:6379");
            IDatabase db = redis.GetDatabase();
            await db.KeyDeleteAsync(key);

            return Ok();
        }
    }
}
