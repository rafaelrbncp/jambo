﻿using Jambo.Producer.Application.Commands.Posts;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Jambo.Domain.Model.Blogs;
using Jambo.Domain.Model.Posts;
using Jambo.Producer.Application.Queries;

namespace Jambo.Producer.UI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class PostsController : Controller
    {
        private readonly IMediator mediator;
        private readonly IPostQueries postQueries;

        public PostsController(IMediator mediator, IPostQueries postQueries)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            this.postQueries = postQueries ?? throw new ArgumentNullException(nameof(postQueries));
        }

        [HttpGet]
        public async Task<IActionResult> GetPosts(Guid blogId)
        {
            var posts = await postQueries.GetPostsAsync(blogId);

            return Ok(posts);
        }

        [HttpGet("{id}", Name = "GetPost")]
        public async Task<IActionResult> Get(Guid id)
        {
            var post = await postQueries.GetPostAsync(id);

            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreatePostCommand command)
        {
            Guid id = await mediator.Send(command);
            return CreatedAtRoute("GetPost", new { id = id }, id);
        }

        [HttpPatch("Comment")]
        public async Task<IActionResult> Comment([FromBody]CreateCommentCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }

        [HttpPatch("Enable")]
        public async Task<IActionResult> Enable([FromBody]EnablePostCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }

        [HttpPatch("Disable")]
        public async Task<IActionResult> Disable([FromBody]DisablePostCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }

        [HttpPatch("Publish")]
        public async Task<IActionResult> Publish([FromBody]PublishPostCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }

        [HttpPatch("Hide")]
        public async Task<IActionResult> Hide([FromBody]HidePostCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }

        [HttpPatch("UpdateContent")]
        public async Task<IActionResult> UpdateContent([FromBody]UpdatePostContentCommand command)
        {
            await mediator.Send(command);
            return (IActionResult)Ok();
        }
    }
}
