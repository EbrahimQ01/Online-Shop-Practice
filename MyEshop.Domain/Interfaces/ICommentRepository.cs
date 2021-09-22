﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEshop.Domain.Interfaces
{
    public interface ICommentRepository
    {
        public int GetCommentCountProductByProductId(int productId);

        public ValueTask<bool> DeleteCommentsByProductIdAsync(int productId);
    }
}
