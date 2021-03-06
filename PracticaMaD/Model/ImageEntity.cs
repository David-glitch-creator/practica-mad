//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Es.Udc.DotNet.PracticaMaD.Model
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    
    public partial class ImageEntity
    {
        public ImageEntity()
        {
            this.Comment = new HashSet<Comment>();
            this.Tag = new HashSet<Tag>();
            this.UserProfile1 = new HashSet<UserProfile>();
        }
    
        public long imageId { get; set; }
        public string title { get; set; }
        public string imageDescription { get; set; }
        public System.DateTime uploadDate { get; set; }
        public string aperture { get; set; }
        public string exposureTime { get; set; }
        public string iso { get; set; }
        public string whiteBalance { get; set; }
        public byte[] imageFile { get; set; }
        public long author { get; set; }
        public long categoryId { get; set; }
    
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_ImageCategory
        /// </summary>
        public virtual Category Category { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_CommentedImage
        /// </summary>
        public virtual ICollection<Comment> Comment { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): FK_ImageAuthor
        /// </summary>
        public virtual UserProfile UserProfile { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): ImageTag
        /// </summary>
        public virtual ICollection<Tag> Tag { get; set; }
        
        /// <summary>
        /// Relationship Name (Foreign Key in ER-Model): Likes
        /// </summary>
        public virtual ICollection<UserProfile> UserProfile1 { get; set; }
    
    	/// <summary>
    	/// A hash code for this instance, suitable for use in hashing algorithms and data structures 
    	/// like a hash table. It uses the Josh Bloch implementation from "Effective Java"
        /// Primary key of entity is not included in the hash calculation to avoid errors
    	/// with Entity Framework creation of key values.
    	/// </summary>
    	/// <returns>
    	/// Returns a hash code for this instance.
    	/// </returns>
    	public override int GetHashCode()
    	{
    	    unchecked
    	    {
    			int multiplier = 31;
    			int hash = GetType().GetHashCode();
    
    			hash = hash * multiplier + (title == null ? 0 : title.GetHashCode());
    			hash = hash * multiplier + (imageDescription == null ? 0 : imageDescription.GetHashCode());
    			hash = hash * multiplier + uploadDate.GetHashCode();
    			hash = hash * multiplier + (aperture == null ? 0 : aperture.GetHashCode());
    			hash = hash * multiplier + (exposureTime == null ? 0 : exposureTime.GetHashCode());
    			hash = hash * multiplier + (iso == null ? 0 : iso.GetHashCode());
    			hash = hash * multiplier + (whiteBalance == null ? 0 : whiteBalance.GetHashCode());
    			hash = hash * multiplier + imageFile.GetHashCode();
    			hash = hash * multiplier + author.GetHashCode();
    			hash = hash * multiplier + categoryId.GetHashCode();
    
    			return hash;
    	    }
    
    	}
        
        /// <summary>
        /// Compare this object against another instance using a value approach (field-by-field) 
        /// </summary>
        /// <remarks>See http://www.loganfranken.com/blog/687/overriding-equals-in-c-part-1/ for detailed info </remarks>
    	public override bool Equals(object obj)
    	{
    
            if (ReferenceEquals(null, obj)) return false;        // Is Null?
            if (ReferenceEquals(this, obj)) return true;         // Is same object?
            if (obj.GetType() != this.GetType()) return false;   // Is same type? 
    
            ImageEntity target = obj as ImageEntity;
    
    		return true
               &&  (this.imageId == target.imageId )       
               &&  (this.title == target.title )       
               &&  (this.imageDescription == target.imageDescription )       
               &&  (this.uploadDate == target.uploadDate )       
               &&  (this.aperture == target.aperture )       
               &&  (this.exposureTime == target.exposureTime )       
               &&  (this.iso == target.iso )       
               &&  (this.whiteBalance == target.whiteBalance )       
               &&  (this.imageFile == target.imageFile )       
               &&  (this.author == target.author )       
               &&  (this.categoryId == target.categoryId )       
               ;
    
        }
    
    
    	public static bool operator ==(ImageEntity  objA, ImageEntity  objB)
        {
            // Check if the objets are the same ImageEntity entity
            if(Object.ReferenceEquals(objA, objB))
                return true;
      
            return objA.Equals(objB);
    }
    
    
    	public static bool operator !=(ImageEntity  objA, ImageEntity  objB)
        {
            return !(objA == objB);
        }
    
    
        /// <summary>
        /// Returns a <see cref="T:System.String"></see> that represents the 
        /// current <see cref="T:System.Object"></see>.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.String"></see> that represents the current 
        /// <see cref="T:System.Object"></see>.
        /// </returns>
    	public override String ToString()
    	{
    	    StringBuilder strImageEntity = new StringBuilder();
    
    		strImageEntity.Append("[ ");
           strImageEntity.Append(" imageId = " + imageId + " | " );       
           strImageEntity.Append(" title = " + title + " | " );       
           strImageEntity.Append(" imageDescription = " + imageDescription + " | " );       
           strImageEntity.Append(" uploadDate = " + uploadDate + " | " );       
           strImageEntity.Append(" aperture = " + aperture + " | " );       
           strImageEntity.Append(" exposureTime = " + exposureTime + " | " );       
           strImageEntity.Append(" iso = " + iso + " | " );       
           strImageEntity.Append(" whiteBalance = " + whiteBalance + " | " );       
           strImageEntity.Append(" imageFile = " + imageFile + " | " );       
           strImageEntity.Append(" author = " + author + " | " );       
           strImageEntity.Append(" categoryId = " + categoryId + " | " );       
            strImageEntity.Append("] ");    
    
    		return strImageEntity.ToString();
        }
    
    
    }
}
