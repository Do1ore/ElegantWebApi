using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElegantWebApi.Application.Features.DeleteDataList
{
    public class DeleteRecordCommandValidator : AbstractValidator<DeleteRecordListCommand>
    {
        public DeleteRecordCommandValidator()
        {
            RuleFor(a => a.Id).NotNull().NotEmpty();
        }
    }
}
