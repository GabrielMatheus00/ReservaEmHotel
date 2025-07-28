using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ReservaHotel.Apresentation.Configuration;
using ReservaHotel.Data.DataAccessLayer;
using ReservaHotel.Data.DataAccessLayer.Repositories.Classes;
using ReservaHotel.Data.Database.Entities;
using ReservaHotel.Data.ResponseMapping;
using ReservaHotel.Domain.Model.DTOs.Usuario;
using ReservaHotel.Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReservaHotel.Services.Services
{
    public class UsuarioService:IUsuarioService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly string _chavePrivada;
        private readonly int _horasExpiracaoToken;
        private readonly IMapper _mapper;
        public UsuarioService(IUnitOfWork unitOfWork, IOptions<Configuracoes> options, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _chavePrivada = options.Value.ChavePrivadaJwt;
            _horasExpiracaoToken = Convert.ToInt32(options.Value.HorasExpiracaoToken);
            _mapper = mapper;
        }

        public async Task<ResponseBase<Guid>> CadastroUsuario(CadastroUsuarioDTO dto)
        {
            ResponseBase<Guid> resposta = new ResponseBase<Guid>();
            try
            {
                string hashSenha = GeraHashDaSenha(dto.Senha);
                Usuario usuario = _mapper.Map<Usuario>(dto);
                _unitOfWork.UsuarioRepository.Adicionar(usuario);
                await _unitOfWork.SalvarAlteracoes();
                resposta.AddSuccess("Usuario criado com sucesso");
                resposta.Data = usuario.Id;
            }
            catch(Exception ex)
            {
                resposta.AddError(ex.Message);
            }
           
            return resposta;
        }

        public ResponseBase<string> Login(LoginDTO dto)
        {
            ResponseBase<string> resposta = new ResponseBase<string>();
            try
            {
                string senhaCriptografada = GeraHashDaSenha(dto.Senha);
                Usuario usuario = _unitOfWork.UsuarioRepository.BuscarUm(u => u.Ativo && (u.Login == dto.Login || u.Email == dto.Login) && u.Senha == senhaCriptografada);
                if (usuario is null)
                    throw new ArgumentException("Não foi possível realizar o login. Por favor verifique as credenciais");
                this.GeraToken(usuario, ref resposta);
            }
            catch (Exception ex) 
            {
                resposta.AddError(ex.Message);
            }
            return resposta;
        }
        private static ClaimsIdentity GerarClaims(Usuario usuario)
        {
            ClaimsIdentity ci = new ClaimsIdentity();
            ci.AddClaim(new Claim(ClaimTypes.Name, usuario.Nome));
            ci.AddClaim(new Claim(ClaimTypes.Email, usuario.Email));
            ci.AddClaim(new Claim("Telefone", usuario.Telefone));
            ci.AddClaim(new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString("dd/MM/yyyy")));
            return ci;
        }

        private void GeraToken(Usuario usuario, ref ResponseBase<string> resposta)
        {
            SecurityToken token;
            try
            {
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                byte[] chave = Encoding.ASCII.GetBytes(_chavePrivada);
                SigningCredentials credenciais = new SigningCredentials(new SymmetricSecurityKey(chave), SecurityAlgorithms.HmacSha256Signature);

                SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
                {
                    SigningCredentials = credenciais,
                    Expires = DateTime.Now.AddHours(_horasExpiracaoToken),
                    Subject = GerarClaims(usuario)
                };
                token = handler.CreateToken(tokenDescriptor);
                resposta.Data = handler.WriteToken(token);

            }
            catch(Exception ex)
            {
                resposta.AddError(ex.Message);
            }
        }

        private string GeraHashDaSenha(string senha)
        {
            var hasher = new PasswordHasher<string>();
            string passwordHash = hasher.HashPassword(null,senha);
            return passwordHash;
        }
    }
}
