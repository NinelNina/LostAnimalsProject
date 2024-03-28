﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostAnimals.Services.Comments;

public interface ICommentService
{
    /// <summary>
    /// Get all breeds
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<CommentModel>> GetAll();

    /// <summary>
    /// Get breed by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<CommentModel> GetById(Guid id);

    /// <summary>
    /// Create a new breed
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<CommentModel> Create(CreateCommentModel model);

    /// <summary>
    /// Delete breed
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);
}
