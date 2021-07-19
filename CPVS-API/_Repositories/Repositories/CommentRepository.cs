using System.Threading.Tasks;
using CPVS_API._Repositories.Interface;
using CPVS_API.Data;
using CPVS_API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using CPVS_API.DTO;
using System.Collections.Generic;
using AutoMapper;

namespace CPVS_API._Repositories.Repositories
{
    public class CommentRepository : ECRepository<Comment>, ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(DataContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

   
    }
}