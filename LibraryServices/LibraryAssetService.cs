using LibraryData;
using LibraryData.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext context;

        public LibraryAssetService(LibraryContext context)
        {
            this.context = context;
        }
        public void Add(LibraryAsset newAsset)
        {
            context.Add(newAsset);
            context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location);
        }

        public LibraryAsset GetById(int id)
        {
            return context.LibraryAssets
                .Include(asset => asset.Status)
                .Include(asset => asset.Location)
                .FirstOrDefault(asset => asset.Id == id);
        }

        public LibraryBranch GetCurrentLocation(int id)
        {
            return context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Location;
        }

        public string GetDeweyIndex(int id)
        {
            if(context.Books.Any(book => book.Id == id))
            {
                return context.Books.FirstOrDefault(book => book.Id == id).DeweyIndex;
            }
            else
            {
                return "";
            }
        }

        public string GetIsbn(int id)
        {
            if (context.Books.Any(book => book.Id == id))
            {
                return context.Books.FirstOrDefault(book => book.Id == id).ISBN;
            }
            else
            {
                return "";
            }
        }

        public string GetTitle(int id)
        {
            return context.LibraryAssets.FirstOrDefault(asset => asset.Id == id).Title;
        }

        public string GetType(int id)
        {
            var book = context.LibraryAssets.OfType<Book>().Where(b => b.Id == id);

            return book.Any() ? "Book" : "Video";
        }

        public string GetAuthorOrDirector(int id)
        {
            var isBook = context.LibraryAssets.OfType<Book>().Where(asset => asset.Id == id).Any();

            return isBook ?
                context.Books.FirstOrDefault(asset => asset.Id == id).Author :
                context.Videos.FirstOrDefault(asset => asset.Id == id).Director
                ?? "Unknown";

        }
    }
}
