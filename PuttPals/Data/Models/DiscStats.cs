namespace PuttPals.Data.Models
{
    public class DiscStats
    {
        private int _speed;
        private int _glide;
        private int _turn;
        private int _fade;

        public int Speed
        {
            get => _speed;
            set
            {
                if (value < 1 || value > 14)
                    throw new ArgumentOutOfRangeException(nameof(Speed), "Speed must be between 1 and 14.");
                _speed = value;
            }
        }

        public int Glide
        {
            get => _glide;
            set
            {
                if (value < 1 || value > 7)
                    throw new ArgumentOutOfRangeException(nameof(Glide), "Glide must be between 1 and 7.");
                _glide = value;
            }
        }

        public int Turn
        {
            get => _turn;
            set
            {
                if (value < -5 || value > 1)
                    throw new ArgumentOutOfRangeException(nameof(Turn), "Turn must be between -5 and 1.");
                _turn = value;
            }
        }

        public int Fade
        {
            get => _fade;
            set
            {
                if (value < 0 || value > 6)
                    throw new ArgumentOutOfRangeException(nameof(Fade), "Fade must be between 0 and 6.");
                _fade = value;
            }
        }
    }
}
