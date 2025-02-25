﻿using ApiJogos.Entities;
using ApiJogos.Exceptions;
using ApiJogos.InputModel;
using ApiJogos.Repositories;
using ApiJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiJogos.Services
{
    public class JogoService : IJogoService
    {
        private readonly IJogoRepository _jogoRepository;

        public JogoService(IJogoRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<JogoViewModel>> Obter(int pagina, int quantidade)
        {
            var jogos = await _jogoRepository.Obter(pagina, quantidade);
            return jogos.Select(jogo => new JogoViewModel
            {

                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Nota = jogo.Nota,
                Tipo = jogo.Tipo
            })
                        .ToList();
        }

        public async Task<JogoViewModel> Obter(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);

            if(jogo == null)
            {
                return null;
            }

            return new JogoViewModel
            {
                Id = jogo.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Nota = jogo.Nota,
                Tipo = jogo.Tipo
            };
        }

        public async Task<JogoViewModel> Inserir(JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(jogo.Nome, jogo.Produtora);

            if(entidadeJogo.Count > 0)
            {
                throw new JogoJaCadastradoException();
            }

            var jogoInsert = new Jogo {
                Id = Guid.NewGuid(),
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Nota = jogo.Nota,
                Tipo = jogo.Tipo
            };

            await _jogoRepository.Inserir(jogoInsert);
            return new JogoViewModel
            {
                Id = jogoInsert.Id,
                Nome = jogo.Nome,
                Produtora = jogo.Produtora,
                Preco = jogo.Preco,
                Nota = jogo.Nota,
                Tipo = jogo.Tipo
            };
        }

        public async Task Atualizar(Guid id,JogoInputModel jogo)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);
            if(entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }
            entidadeJogo.Nome = jogo.Nome;
            entidadeJogo.Produtora = jogo.Produtora;
            entidadeJogo.Preco = jogo.Preco;
            entidadeJogo.Preco = jogo.Preco;
            entidadeJogo.Nota = jogo.Nota;
            entidadeJogo.Tipo = jogo.Tipo;

            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeJogo = await _jogoRepository.Obter(id);

            if(entidadeJogo == null)
            {
                throw new JogoNaoCadastradoException();
            }
            entidadeJogo.Preco = preco;
            await _jogoRepository.Atualizar(entidadeJogo);
        }

        public async Task Remover(Guid id)
        {
            var jogo = await _jogoRepository.Obter(id);
            if(jogo == null)
            {
                throw new JogoNaoCadastradoException();
            }
            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}
